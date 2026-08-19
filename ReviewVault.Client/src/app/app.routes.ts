import { Routes } from '@angular/router';
import { authGuard , adminGuard} from './core/guards/auth.guard';

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
        canActivate: [adminGuard]
    },
    {
        path: 'admin/create-post',
        loadComponent: () => import('./pages/admin/create-post/create-post')
            .then(m => m.CreatePost),
        canActivate: [adminGuard]
    },
    {
        path: 'admin/edit-post/:id',
        loadComponent: () => import('./pages/admin/edit-post/edit-post')
            .then(m => m.EditPost),
        canActivate: [adminGuard]
    },
    {
        path: 'register',
        loadComponent: () => import('./pages/register/register')
            .then(m => m.Register)
    },
    {
    path: 'trending',
    loadComponent: () => import('./pages/trending/trending')
        .then(m => m.Trending)
    },
    {
    path: 'profile',
    loadComponent: () => import('./pages/profile/profile')
        .then(m => m.Profile),
    canActivate: [authGuard]
    },
    {
    path: 'search',
    loadComponent: () => import('./pages/search/search')
        .then(m => m.Search)
    },
    {
    path: 'about',
    loadComponent: () => import('./pages/about/about')
        .then(m => m.About)
    },

    // Wildcard — any unknown URL goes to home
    {
    path: '**',
    loadComponent: () => import('./pages/not-found/not-found')
        .then(m => m.NotFound)
    }

    
];
