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
            component: import('./views/user-view/storage-view/storage-view-create/storage-view-create'),
            title: "StorageCreate",
        },
        {
            id: 'storage-edit',
            path: '/storage/edit/:id',
            component: import('./views/user-view/storage-view/storage-view-edit/storage-view-edit'),
            title: "StorageEdit",
        },
        {
            id: 'storage-detail',
            path: '/storage/details/:id',
            component: import('./views/user-view/storage-view/storage-view-detail/storage-view-detail'),
            title: "StorageDetail",
        }, {
            id: 'storage-delete',
            path: '/storage/delete/:id',
            component: import('./views/user-view/storage-view/storage-view-delete/storage-view-delete'),
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
        , {
            id: 'image-add',
            path: '/image/create/:id',
            component: import('./views/user-view/image-view/image-view-add'),
            title: "Item Create",
        },
    ]
})

export class MyApp {

}
