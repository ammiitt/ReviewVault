import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
    {
    path: 'admin/create-post',
    //component: CreatePostComponent,
    canActivate: [authGuard]        // ← guard checks before loading
    }
];
