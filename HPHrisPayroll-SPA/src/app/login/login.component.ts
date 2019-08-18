import { Component, OnInit, ViewChild } from '@angular/core';
import { LoginModel } from '../_model/loginModel';
import { DxFormComponent } from 'devextreme-angular';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  @ViewChild(DxFormComponent, {static: false}) form: DxFormComponent;
  loginModel: LoginModel = { username: '', password: '' };
  labelLocation: string;
  showColon: boolean;
  loginButtonOptions: any = {
    text: 'LOGIN',
    type: 'default',
    width: '100%',
    useSubmitBehavior: true
  };
  showError = false;
  isLoadingPanelVisible = false;
  testFetch: string;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.labelLocation = 'top';
    this.showColon = true;
  }

  onFormSubmit(e) {
    this.isLoadingPanelVisible = true;
    const formData = this.form.instance.option('formData');
    this.authService.login(formData).subscribe(() => {

    }, error => {
      this.showError = true;
      this.isLoadingPanelVisible = false;
    }, () => {
      this.isLoadingPanelVisible = false;
    });

  }

}
