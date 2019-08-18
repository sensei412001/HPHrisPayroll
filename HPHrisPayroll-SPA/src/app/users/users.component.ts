import CustomStore from 'devextreme/data/custom_store';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { createStore } from 'devextreme-aspnet-data-nojquery';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  url = environment.apiUrl + 'users/';
  urlUserGroup = environment.apiUrl + 'userGroup/';
  urlEmp = environment.apiUrl + 'employee/ForLookup/';
  urlAccess = environment.apiUrl + 'UserGroupAccess/';

  dataSource: CustomStore;
  dataSourceUserGroup: CustomStore;
  dataSourceEmp: CustomStore;
  dataSourceAccess: CustomStore;

  popupVisible = false;
  userGroupData: any = { id: null, name: '' };

  constructor(private authService: AuthService) {
    this.popCompanyDataGridLookUpUserGroups = this.popCompanyDataGridLookUpUserGroups.bind(this);
    this.popCompanyDataGridLookUpEmployee = this.popCompanyDataGridLookUpEmployee.bind(this);
    this.showAccess = this.showAccess.bind(this);
  }

  ngOnInit() {
    this.popDataGrid();
    this.popCompanyDataGridLookUpUserGroups();
    this.popCompanyDataGridLookUpEmployee();
  }

  popDataGrid() {
    this.dataSource = createStore({
      key: 'userName',
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

  popCompanyDataGridLookUpUserGroups() {
    this.dataSourceUserGroup = createStore({
      key: 'userGroupId',
      loadUrl: this.urlUserGroup,
      onBeforeSend: function(r, s) {
        const token = localStorage.getItem('token');
        s.headers = { Authorization: 'Bearer ' + token };
      }
    });
  }

  popCompanyDataGridLookUpEmployee() {
    this.dataSourceEmp = createStore({
      key: 'employeeNo',
      loadUrl: this.urlEmp,
      onBeforeSend: function(r, s) {
        const token = localStorage.getItem('token');
        s.headers = { Authorization: 'Bearer ' + token };
      }
    });
  }

  popDataGridAccess(id: number) {
    this.dataSourceAccess = createStore({
      key: 'userGroupAccessId',
      loadUrl: this.urlAccess + 'Access/' + id,
      updateUrl: this.urlAccess,
      onBeforeSend: function(r, s) {
        const token = localStorage.getItem('token');
        s.headers = { Authorization: 'Bearer ' + token };
      }
    });
  }

  onInitNewRow(e: any) {
    const date = new Date();

    const dd = date.getDate();
    const mm = date.getMonth() + 1; // January is 0!
    const yy = date.getFullYear();

    const expDate = new Date();
    expDate.setDate(date.getDate() + 120);

    e.data.dateCreated = mm + '/' + dd + '/' + yy;
    e.data.passwordExpiration = expDate.getMonth() + '/' + expDate.getDate() + '/' + expDate.getFullYear();
    e.data.isEnable = true;
    e.data.createdBy = this.authService.decodedToken.nameid;
  }

  showAccess(e) {
    this.popupVisible = true;

    const data = e.row.data;
    const id = data.userGroupId;
    const name = data.userGroupName;

    this.userGroupData = {
      id: id,
      name: name
    };

    this.popDataGridAccess(id);

  }

}
