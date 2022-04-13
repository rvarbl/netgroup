import { HttpClient } from "aurelia";
import { I_Image } from "../../domain/files/I_Image";
import { IUser } from "../../domain/identity/IUser";

export class ImageService {
    httpClient: HttpClient = new HttpClient();
    async getImage(path: string, user: IUser) {
        console.log("INGHERINHEREINEHRE ");
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        let dto: I_Image = { path: path }
        let response = await this.httpClient.post(`https://localhost:7286/api/image/get`, JSON.stringify(dto));
        console.log("INGHERINHEREINEHRE2 ", response);
        let json = await response.json();
        console.log("INGHERINHEREINEHRE3 ", json);
        let data: I_Image = json;

        
        return data;
    }

    async addImage(image: I_Image, user: IUser) {
        console.log("STORAGEITEMPOST: ", JSON.stringify(image));
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


                let response = await this.httpClient.post(`https://localhost:7286/api/image/add`, JSON.stringify(image));
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

        await this.httpClient.delete(`https://localhost:7286/api/image/delete`, JSON.stringify(image));
    }
}