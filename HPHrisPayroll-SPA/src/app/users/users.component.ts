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
  pwrdColVisible = true;

  passwordMode: string;
  passwordButton: any;
  passwordPattern: any = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/;

  constructor(private authService: AuthService) {
    this.popCompanyDataGridLookUpUserGroups = this.popCompanyDataGridLookUpUserGroups.bind(this);
    this.popCompanyDataGridLookUpEmployee = this.popCompanyDataGridLookUpEmployee.bind(this);
    this.showAccess = this.showAccess.bind(this);

    this.passwordMode = 'password';
    this.passwordButton = {
      // tslint:disable-next-line: max-line-length
      icon: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACQAAAAkCAYAAADhAJiYAAAABmJLR0QA/wD/AP+gvaeTAAAACXBIWXMAAAsTAAALEwEAmpwYAAAB7klEQVRYw+2YP0tcQRTFz65xFVJZpBBS2O2qVSrRUkwqYfUDpBbWQu3ELt/HLRQ/Q8RCGxVJrRDEwj9sTATxZ/Hugo4zL/NmV1xhD9xi59177pl9986fVwLUSyi/tYC+oL6gbuNDYtyUpLqkaUmfJY3a+G9JZ5J2JW1J2ivMDBSxeWCfeBxYTHSOWMcRYLOAEBebxtEVQWPASQdi2jgxro4E1YDTQIJjYM18hszGbew4EHNq/kmCvgDnHtI7YBko58SWgSXg1hN/btyFBM0AlwExczG1YDZrMS4uLUeUoDmgFfjLGwXEtG05wNXyTc4NXgzMCOAIGHD8q0ATuDZrempkwGJ9+AfUQ4K+A/eEseqZ/UbgdUw4fqs5vPeW+5mgBvBAPkLd8cPju+341P7D/WAaJGCdOFQI14kr6o/zvBKZYz11L5Okv5KGA89Kzu9K0b0s5ZXt5PjuOL6TRV5ZalFP4F+rrnhZ1Cs5vN6ijmn7Q162/ThZq9+YNW3MbfvDAOed5cxdGL+RFaUPKQtjI8DVAr66/u9i6+jJzTXm+HFEVqxVYBD4SNZNKzk109HxoycPaG0bIeugVDTp4hH2qdXJDu6xOAAWiuQoQdLHhvY1aEZSVdInG7+Q9EvSz9RrUKqgV0PP3Vz7gvqCOsUj+CxC9LB1Dc8AAAASdEVYdEVYSUY6T3JpZW50YXRpb24AMYRY7O8AAAAASUVORK5CYII=',
      type: 'default',
      onClick: () => {
          this.passwordMode = this.passwordMode === 'text' ? 'password' : 'text';
      }
  };
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
    this.pwrdColVisible = true;

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

  onEditorPreparing(e: any) {
    if (e.parentType === 'dataRow' && e.dataField === 'syek' && !e.row.inserted) {
      this.pwrdColVisible = false;
    }
    if (e.parentType === 'dataRow' && e.dataField === 'userName' && !e.row.inserted) {
      e.editorOptions.readOnly = true;
    }
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

  setUsernameValue(rowData: any, value: any): void {
    rowData.userName = value;
    (<any>this).defaultSetCellValue(rowData, value);
  }


}
