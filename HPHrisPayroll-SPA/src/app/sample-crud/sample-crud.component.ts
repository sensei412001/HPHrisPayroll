import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

import { createStore } from 'devextreme-aspnet-data-nojquery';
import CustomStore from 'devextreme/data/custom_store';

@Component({
  selector: 'app-sample-crud',
  templateUrl: './sample-crud.component.html',
  styleUrls: ['./sample-crud.component.css']
})
export class SampleCrudComponent implements OnInit {
  url = environment.apiUrl + '';
  dataSource: CustomStore;

  constructor(private http: HttpClient) { }

  ngOnInit() {

  }



}
