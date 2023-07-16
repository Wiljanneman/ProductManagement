import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResponsiveHelperComponent } from './components/responsive-helper/responsive-helper.component';
import { ClickOutsideDirective } from './directives/click-outside.directive';
import { HttpClient } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import {MatDialogModule} from '@angular/material/dialog';
import {MatPaginatorModule} from '@angular/material/paginator';

@NgModule({
  declarations: [ResponsiveHelperComponent, ClickOutsideDirective, ConfirmationDialogComponent],
  imports: [CommonModule, MatSnackBarModule, MatTableModule, MatCheckboxModule, MatIconModule,  MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButtonModule, MatDialogModule, MatPaginatorModule],
  exports: [ResponsiveHelperComponent, ClickOutsideDirective, MatSnackBarModule, MatTableModule, MatCheckboxModule, MatIconModule,  MatFormFieldModule, MatInputModule, ReactiveFormsModule, MatButtonModule, MatDialogModule, MatPaginatorModule],
  providers: [HttpClient]
})
export class SharedModule {}
