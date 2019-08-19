import CustomStore from 'devextreme/data/custom_store';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { createStore } from 'devextreme-aspnet-data-nojquery';
import { AuthService } from '../_services/auth.service';

@Component({
  // tslint:disable-next-line: component-selector
  selector: 'app-main-employeeNumbering',
  templateUrl: './main-employeeNumbering.component.html',
  styleUrls: ['./main-employeeNumbering.component.css']
})
export class MainEmployeeNumberingComponent implements OnInit {
  url = environment.apiUrl + 'employeeConfig/';
  urlCompany = environment.apiUrl + 'company/';

  dataSource: CustomStore;
  dataSourceCompanies: CustomStore;

  constructor(private authService: AuthService) {
    this.popCompanyDataGridLookUpCompanies = this.popCompanyDataGridLookUpCompanies.bind(this);
  }

  ngOnInit() {
    this.popDataGrid();
    this.popCompanyDataGridLookUpCompanies();
  }

  popDataGrid() {
    this.dataSource = createStore({
      key: 'employeeNoConfigId',
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

  popCompanyDataGridLookUpCompanies() {
    this.dataSourceCompanies = createStore({
      key: 'companyCode',
      loadUrl: this.urlCompany + 'lookup/',
      onBeforeSend: function(r, s) {
        const token = localStorage.getItem('token');
        s.headers = { Authorization: 'Bearer ' + token };
      }
    });
  }

  onInitNewRow(e: any) {
    const dte = new Date();
    const dd = dte.getDate();
    const mm = dte.getMonth() + 1; // January is 0!
    const yy = dte.getFullYear();

    e.data.dateCreated = mm + '/' + dd + '/' + yy;
    e.data.createdBy = this.authService.decodedToken.nameid;
  }

  onEditorPreparing(e: any) {
    if (e.parentType === 'dataRow' && e.dataField === 'EmployeeNoConfigId' && !e.row.inserted) {
        e.editorOptions.readOnly = true;
    }
  }


}
