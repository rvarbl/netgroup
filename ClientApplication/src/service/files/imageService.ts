import { HttpClient } from "aurelia";
import { I_Image } from "../../domain/files/I_Image";
import { IUser } from "../../domain/identity/IUser";

export class ImageService {
    httpClient: HttpClient = new HttpClient();
    url: string = "https://localhost:7286";
    async getImage(path: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        let dto: I_Image = { path: path }
        let response = await this.httpClient.post(this.url + "/api/image/get", JSON.stringify(dto));
        let json = await response.json();
        let data: I_Image = json;


        return data;
    }

    async addImage(image: I_Image, user: IUser) {
        if (user !== undefined && image !== undefined) {
            try {
                this.httpClient.configure(config => {
                    return config.withInterceptor({
                        request(request) {
                            request.headers.set('Authorization', 'Bearer ' + user?.token);
                            return request;
                        }
                    });
                })


                let response = await this.httpClient.post(this.url + "/api/image/add", JSON.stringify(image));
                return response.status;
            }
            catch {
                //mingi error
                return;
            }
        }
    }

    async deleteImage(image: I_Image, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        await this.httpClient.delete(this.url + "/api/image/delete", JSON.stringify(image));
    }
}