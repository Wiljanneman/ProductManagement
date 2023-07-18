import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../auth.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss'],
})
export class SignInComponent implements OnInit {
  form!: FormGroup;
  submitted = false;
  passwordTextType!: boolean;

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _router: Router,
    private _authService: AuthService,
    private _progressBarService: ProgressBarService,
    private snackbarService: SnackbarService) {}

  ngOnInit(): void {
    if (this._authService.isLoggedIn()) {
      
    }
    this.form = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  get f() {
    return this.form.controls;
  }

  togglePasswordTextType() {
    this.passwordTextType = !this.passwordTextType;
  }

  onSubmit() {
    
    this.submitted = true;
    const { email, password } = this.form.value;

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }
    this._progressBarService.start();
    this._authService.login(email, password).subscribe(res => {
      this._progressBarService.complete();
      if (res.status === 200) {
        this._router.navigate(['/']);
      } else {
        this.snackbarService.show('Login failed - ' + res?.message)
      }
    }, () => {
      this._progressBarService.complete();
    })

  }
}
