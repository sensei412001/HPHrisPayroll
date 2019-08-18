import { createStore } from 'devextreme-aspnet-data-nojquery';
import CustomStore from 'devextreme/data/custom_store';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-maint-company',
  templateUrl: './maint-company.component.html',
  styleUrls: ['./maint-company.component.css']
})
export class MaintCompanyComponent implements OnInit {
  url = environment.apiUrl + 'Company';
  dataSource: CustomStore;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.popDataGrid();
  }

  popDataGrid() {
    this.dataSource = createStore({
      key: 'companyCode',
      loadUrl: this.url,
      insertUrl: this.url,
      updateUrl: this.url,
      deleteUrl: this.url,
      onBeforeSend: function(r, s) {
        const token = localStorage.getItem('token');
        s.headers = { Authorization: 'Bearer ' + token };
      }
    });
  }

  onEditorPreparing(e: any) {
    if (e.parentType === 'dataRow' && e.dataField === 'companyCode' && !e.row.inserted) {
        e.editorOptions.disabled = true;
    }
    if (e.parentType === 'dataRow' && e.dataField === 'dateCreated' && !e.row.inserted) {
        e.editorOptions.disabled = true;
    }
  }

  onInitNewRow(e: any) {
    const dte = new Date();
    const dd = dte.getDate();
    const mm = dte.getMonth() + 1; // January is 0!
    const yy = dte.getFullYear();

    e.data.dateCreated = mm + '/' + dd + '/' + yy;
    e.data.createdBy = this.authService.decodedToken.nameid;
  }

}
