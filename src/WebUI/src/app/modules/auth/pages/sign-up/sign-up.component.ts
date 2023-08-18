import { Component, OnInit, ViewChild } from '@angular/core';
import { Form, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss'],
})
export class SignUpComponent implements OnInit {
  @ViewChild('stepper')
  private stepper!: MatStepper;
  accountType!: number;

  constructor(private formBuilder: FormBuilder, private _snackbarService: SnackbarService) {

  }

  ngOnInit(): void {}

  onProductSelected(val: number) {
    this.accountType = val;
    this.stepper.next();
  }
}
