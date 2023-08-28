import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostModel } from 'src/app/core/models/post.model';

@Component({
  selector: 'app-post-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './post-card.component.html',
  styleUrls: ['./post-card.component.css']
})
export class PostCardComponent {
  @Input() post : PostModel | null = null;
}