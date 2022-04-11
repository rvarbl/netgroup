import { route } from "aurelia";

@route({
    routes: [
        {
            id: 'main',
            path: ['/', '/main'],
            component: import('./views/main-view/main-view'),
            title: "Home"
        },
        {
            id: 'storage',
            path: '/storage',
            component: import('./views/user-view/user-view'),
            title: "Storages"
        },
        {
            id: 'storage-edit',
            path: '/storage/edit/:id',
            component: import('./views/user-view/storage-view/storage-edit/storage-edit-view'),
            title: "StorageEdit",
        },
        {
            id: 'storage-detail',
            path: '/storage/details/:id',
            component: import('./views/user-view/storage-view/storage-detail/storage-detail-view'),
            title: "StorageDetail",
        },{
            id: 'storage-delete',
            path: '/storage/delete/:id',
            component: import('./views/user-view/storage-view/storage-delete/storage-delete-view'),
            title: "StorageDelete",
        },
        {
            id: 'register',
            path: '/register',
            component: import('./views/register-view/register-view'),
            title: "Register"
        },
        {
            id: '/login',
            path: 'login',
            component: import('./views/login-view/login-view'),
            title: "Login"
        }

    ]
})
export class MyApp {

}
