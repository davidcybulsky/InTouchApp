import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserModel} from "../../../core/models/user.model";
import {SearchService} from "../../../core/services/search.service";
import {ActivatedRoute} from "@angular/router";
import {Observable, ReplaySubject, takeUntil} from "rxjs";

@Component({
  selector: 'app-users-search',
  templateUrl: './users-search.component.html',
  styleUrls: ['./users-search.component.css']
})
export class UsersSearchComponent implements OnInit, OnDestroy{

  users: UserModel[] | null = null;
  destroy$ : ReplaySubject<boolean> = new ReplaySubject(1);

  constructor(private searchService: SearchService,
              private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(value => {
      this.searchService.getUsers(value["request"])
        .pipe(takeUntil(this.destroy$))
        .subscribe( response => {
        this.users = response;
      })
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next(true)
    this.destroy$.complete()
  }

}
