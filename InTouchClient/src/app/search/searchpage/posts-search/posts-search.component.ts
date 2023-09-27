import {Component} from '@angular/core';
import {ReplaySubject, takeUntil} from "rxjs";
import {SearchService} from "../../../core/services/search.service";
import {ActivatedRoute} from "@angular/router";
import {PostModel} from "../../../core/models/post.model";

@Component({
  selector: 'app-posts-search',
  templateUrl: './posts-search.component.html',
  styleUrls: ['./posts-search.component.css']
})
export class PostsSearchComponent {
  posts: PostModel[] | null = null;
  destroy$: ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private searchService: SearchService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(value => {
      this.searchService.getPosts(value["request"])
        .pipe(takeUntil(this.destroy$))
        .subscribe(response => {
          this.posts = response;
        })
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true)
    this.destroy$.complete()
  }

}
