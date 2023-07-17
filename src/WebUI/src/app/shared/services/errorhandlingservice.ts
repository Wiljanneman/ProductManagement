import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlingService {

  handleHttpError(error: any) {
    if (error.status === 403 || error.status === 0) {

      const errorMessage = 'You are unauthorized to perform this action.';
      return throwError({ message: errorMessage});

    } else {

      return throwError(error);
    }
  }
}
