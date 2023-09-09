import { Component} from '@angular/core';
import { CommonModule } from '@angular/common';
import { PostCardComponent } from 'src/app/shared/components/post/post-page/post-card/post-card.component';

@Component({
  selector: 'app-post-details',
  standalone: true,
  imports: [
    CommonModule,
    PostCardComponent
  ],
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.css']
})
export class PostDetailsComponent {

}
