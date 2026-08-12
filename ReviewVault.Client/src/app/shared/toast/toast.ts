import { Component, OnInit } from '@angular/core';
import { Toast,ToastService } from '../../core/services/toast';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [CommonModule],
  templateUrl: './toast.html',
  styleUrl: './toast.css',
})
export class Toaster implements OnInit {

    toasts: Toast[] = [];
    constructor(private toastService: ToastService) { }

    ngOnInit(): void {
        this.toastService.toasts$.subscribe(toasts => {
            this.toasts = toasts;
        });
    }

    dismiss(id: number): void {
        this.toastService.remove(id);
    }

    getToastClass(type: string): string {
        const classes: { [key: string]: string } = {
            success: 'bg-success text-white',
            error: 'bg-danger text-white',
            warning: 'bg-warning text-dark',
            info: 'bg-primary text-white'
        };
        return classes[type] || '';
    }

    getHeaderClass(type: string): string {
        const classes: { [key: string]: string } = {
            success: 'bg-success text-white',
            error: 'bg-danger text-white',
            warning: 'bg-warning text-dark',
            info: 'bg-primary text-white'
        };
        return classes[type] || '';
    }

    getIconClass(type: string): string {
        const icons: { [key: string]: string } = {
            success: 'bi-check-circle-fill',
            error: 'bi-exclamation-triangle-fill',
            warning: 'bi-exclamation-circle-fill',
            info: 'bi-info-circle-fill'
        };
        return icons[type] || 'bi-info-circle';
    }

    getProgressClass(type: string): string {
        const classes: { [key: string]: string } = {
            success: 'progress-success',
            error: 'progress-error',
            warning: 'progress-warning',
            info: 'progress-info'
        };
        return classes[type] || '';
    }
}
