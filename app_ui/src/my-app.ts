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
        },
        //admin
        {
            id: 'admin',
            path: '/admin',
            component: import('./views/adminView/admin-view'),
            title: "AdminView"
        },
        //storage
        {
            id: 'storage',
            path: '/storage',
            component: import('./views/user-view/user-view'),
            title: "Storages"
        },
        {
            id: 'storage-create',
            path: '/storage/create',
            component: import('./views/user-view/storage-view/storage-create/storage-create-view'),
            title: "StorageCreate",
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
        }, {
            id: 'storage-delete',
            path: '/storage/delete/:id',
            component: import('./views/user-view/storage-view/storage-delete/storage-delete-view'),
            title: "StorageDelete",
        },
        //items
        {
            id: 'item-detail',
            path: '/item/details/:id',
            component: import('./views/user-view/item-view/item-view-detail/item-view-detail'),
            title: "ItemDetail",
        }, {
            id: 'item-delete',
            path: '/item/delete/:id',
            component: import('./views/user-view/item-view/item-view-delete/item-view-delete'),
            title: "ItemDelete",
        }
        , {
            id: 'item-edit',
            path: '/item/edit/:id',
            component: import('./views/user-view/item-view/item-view-edit/item-view-edit'),
            title: "ItemEdit",
        }, {
            id: 'item-create',
            path: '/item/create/:id',
            component: import('./views/user-view/item-view/item-view-create/item-view-create'),
            title: "Item Create",
        },
        //attributes
        {
            id: 'attribute-delete',
            path: '/attribute/delete/:id',
            component: import('./views/user-view/attribute-view/attribute-view-delete/attribute-view-delete'),
            title: "Attribute",
        }
        , {
            id: 'attribute-create',
            path: '/attribute/create/:id',
            component: import('./views/user-view/attribute-view/attribute-view-create/attribute-view-create'),
            title: "Attribute Create",
        }

    ]
})
export class MyApp {

}
