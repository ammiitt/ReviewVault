import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { PostResponse } from '../../core/models/post.model';

@Component({
  selector: 'app-post-card',
  imports: [CommonModule, RouterModule],
  templateUrl: './post-card.html',
  styleUrl: './post-card.css',
})
export class PostCard {
     @Input() post!: PostResponse;

    constructor(private router: Router) { }

    // Navigate to full post when card is clicked
    goToPost(): void {
        this.router.navigate(['/post', this.post.slug]);
    }

    // Different colors based on rating
    getRatingClass(): string {
        switch (this.post.rating) {
            case 5: return 'bg-success';     // Masterpiece = green
            case 4: return 'bg-primary';     // Good = blue
            case 3: return 'bg-warning';     // Average = yellow
            case 2: return 'bg-orange';      // Bad = orange
            case 1: return 'bg-danger';      // Terrible = red
            default: return 'bg-secondary';
        }
    }
}
