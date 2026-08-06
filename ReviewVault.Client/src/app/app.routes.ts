import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    // Public routes
    {
        path: '',
        loadComponent: () => import('./pages/home/home')
            .then(m => m.Home)
    },
    {
        path: 'post/:slug',
        loadComponent: () => import('./pages/post-detail/post-detail')
            .then(m => m.PostDetail)
    },
    {
        path: 'category/:name',
        loadComponent: () => import('./pages/category/category')
            .then(m => m.Category)
    },
    {
        path: 'login',
        loadComponent: () => import('./pages/login/login')
            .then(m => m.Login)
    },

    // Admin routes — protected by authGuard
    {
        path: 'admin/dashboard',
        loadComponent: () => import('./pages/admin/dashboard/dashboard')
            .then(m => m.Dashboard),
        canActivate: [authGuard]
    },
    {
        path: 'admin/create-post',
        loadComponent: () => import('./pages/admin/create-post/create-post')
            .then(m => m.CreatePost),
        canActivate: [authGuard]
    },
    {
        path: 'admin/edit-post/:id',
        loadComponent: () => import('./pages/admin/edit-post/edit-post')
            .then(m => m.EditPost),
        canActivate: [authGuard]
    },
    {
    path: 'trending',
    loadComponent: () => import('./pages/trending/trending')
        .then(m => m.Trending)
    },

    // Wildcard — any unknown URL goes to home
    { path: '**', redirectTo: '' }
    
];
