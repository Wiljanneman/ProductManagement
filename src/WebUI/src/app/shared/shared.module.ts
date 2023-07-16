import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResponsiveHelperComponent } from './components/responsive-helper/responsive-helper.component';
import { ClickOutsideDirective } from './directives/click-outside.directive';
import { HttpClient } from '@angular/common/http';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatIconModule} from '@angular/material/icon';

@NgModule({
  declarations: [ResponsiveHelperComponent, ClickOutsideDirective],
  imports: [CommonModule, MatSnackBarModule, MatTableModule, MatCheckboxModule, MatIconModule],
  exports: [ResponsiveHelperComponent, ClickOutsideDirective, MatSnackBarModule, MatTableModule, MatCheckboxModule, MatIconModule],
  providers: [HttpClient]
})
export class SharedModule {}
