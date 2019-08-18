import { createStore } from 'devextreme-aspnet-data-nojquery';
import CustomStore from 'devextreme/data/custom_store';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-user-groups',
  templateUrl: './user-groups.component.html',
  styleUrls: ['./user-groups.component.css']
})
export class UserGroupsComponent implements OnInit {
  urlGroup = environment.apiUrl + 'userGroup/';
  urlAccess = environment.apiUrl + 'UserGroupAccess/';

  dataSource: CustomStore;
  dataSourceAccess: CustomStore;

  popupVisible = false;
  userGroupData: any = { id: null, name: '' };

  constructor(private authService: AuthService) {
    this.showPopup = this.showPopup.bind(this);
    this.popDataGridAccess = this.popDataGridAccess.bind(this);
  }

  ngOnInit() {
    this.popDataGrid();
  }

  popDataGrid() {
    this.dataSource = createStore({
      key: 'userGroupId',
      loadUrl: this.urlGroup,
      insertUrl: this.urlGroup,
      updateUrl: this.urlGroup,
      deleteUrl: this.urlGroup,
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
    const dte = new Date();
    const dd = dte.getDate();
    const mm = dte.getMonth() + 1; // January is 0!
    const yy = dte.getFullYear();

    e.data.dateCreated = mm + '/' + dd + '/' + yy;
    e.data.createdBy = this.authService.decodedToken.nameid;
  }

  showPopup(e) {
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
