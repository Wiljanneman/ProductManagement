import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';

@Component({
  selector: 'app-registration-details-component',
  templateUrl: './registration-details-component.component.html',
  styleUrls: ['./registration-details-component.component.scss']
})
export class RegistrationDetailsComponentComponent implements OnInit {
 @Output() formComplete = new EventEmitter<any>();
 registrationForm!: FormGroup;
  constructor(private formBuilder: FormBuilder, private _snackbarService: SnackbarService) {
    this.registrationForm = formBuilder.group({
      email: [null,Validators.required],
      password: [null,Validators.required],
      passwordConfirm: [null,Validators.required],
      terms: [false,Validators.required]
    })
  }
  ngOnInit(): void {
    // Load the form data from localStorage during component initialization
    const savedFormValue = JSON.parse(localStorage.getItem('savedForm') || '{}');
    this.registrationForm.patchValue(savedFormValue);
  }
  completeForm() {
    if (!this.registrationForm.valid) {
      this._snackbarService.show('Please fill in all required fields.');
      return;
    }
    this.saveFormToLocalStorage();

  }

  saveFormToLocalStorage() {
    const formValue = this.registrationForm.value;
    localStorage.setItem('savedForm', JSON.stringify(formValue));
  }
}
