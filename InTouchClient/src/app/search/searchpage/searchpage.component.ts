import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";

@Component({
  selector: 'app-searchpage',
  templateUrl: './searchpage.component.html',
  styleUrls: ['./searchpage.component.css']
})
export class SearchpageComponent implements OnInit {
  searchForm!: FormGroup

  constructor(private formBuilder: FormBuilder,
              private router: Router) {
  }

  ngOnInit(): void {
    this.searchForm = this.formBuilder.group({
      request: ['', [Validators.required]]
    })
  }

  onSearch() {
    const request = this.searchForm.get("request")?.value
    this.router.navigate([], {queryParams: {request: request}})
  }
}
