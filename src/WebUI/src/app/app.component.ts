import { Component, OnInit } from '@angular/core';
import { ThemeService } from './core/services/theme.service';
import { ProgressBarService } from './shared/services/progress-bar.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit{
  isLoading$: Observable<boolean | null> = of(false);

  title = 'Product Management Dashboard';

  constructor(public themeService: ThemeService, private progressBarService: ProgressBarService) {}
  ngOnInit(): void {
    this.isLoading$ = this.progressBarService.loading$;
  }


}
