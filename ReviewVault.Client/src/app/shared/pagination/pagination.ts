import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  imports: [CommonModule],
  templateUrl: './pagination.html',
  styleUrl: './pagination.css',
})
export class Pagination {
     @Input() currentPage = 1;
    @Input() totalPages = 1;
    @Input() totalCount = 0;

    // Sends page number to parent when user clicks
    @Output() pageChange = new EventEmitter<number>();

    // Generate visible page numbers around current page
    getVisiblePages(): number[] {
        const pages: number[] = [];
        const start = Math.max(1, this.currentPage - 2);
        const end = Math.min(this.totalPages, this.currentPage + 2);

        for (let i = start; i <= end; i++) {
            pages.push(i);
        }
        return pages;
    }

    goToPage(page: number): void {
        if (page < 1 || page > this.totalPages || page === this.currentPage) return;
        this.pageChange.emit(page);
    }
}
