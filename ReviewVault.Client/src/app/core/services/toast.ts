import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';


export interface Toast 
{
    id: number;
    message: string;
    title: string;
    type: 'success' | 'error' | 'warning' | 'info';
}

@Injectable({
  providedIn: 'root',
})

export class ToastService {
  private toasts: Toast[] = [];
    private toastsSubject = new BehaviorSubject<Toast[]>([]);
    toasts$ = this.toastsSubject.asObservable();
    private counter = 0;

    success(message: string, title: string = 'Success'): void {
        this.show(message, title, 'success');
    }

    error(message: string, title: string = 'Error'): void {
        this.show(message, title, 'error');
    }

    warning(message: string, title: string = 'Warning'): void {
        this.show(message, title, 'warning');
    }

    info(message: string, title: string = 'Info'): void {
        this.show(message, title, 'info');
    }

    private show(message: string, title: string, type: Toast['type']): void {
        const id = ++this.counter;
        const toast: Toast = { id, message, title, type };

        this.toasts.push(toast);
        this.toastsSubject.next([...this.toasts]);

        // Auto-remove after 3 seconds
        setTimeout(() => this.remove(id), 3000);
    }

    remove(id: number): void {
        this.toasts = this.toasts.filter(t => t.id !== id);
        this.toastsSubject.next([...this.toasts]);
    }
}
