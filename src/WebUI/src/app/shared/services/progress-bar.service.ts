import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProgressBarService {
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();

  start(): void {
    this.loadingSubject.next(true);
  }

  complete(): void {
    this.loadingSubject.next(false);
  }
}
