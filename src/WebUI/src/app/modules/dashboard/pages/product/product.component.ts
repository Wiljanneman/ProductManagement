import { Component, OnInit, ViewChild } from '@angular/core';
import { Product } from '../../models/Product';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from '../product.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MatPaginator } from '@angular/material/paginator';
import { ProgressBarService } from 'src/app/shared/services/progress-bar.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent implements OnInit {
  displayedColumns = ['id','name','description','incVatAmount','excVatAmount','qty','isSale', 'actions'];
  dataSource = new MatTableDataSource<Product>();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  constructor(private _progressBarService: ProgressBarService ,private _productService: ProductService, private _snackbarService: SnackbarService, private _router: Router, private _matDialog: MatDialog) {

  }

  ngOnInit(): void {
    this.getProducts();
  }
  getProducts() {
    this._progressBarService.start();
    this._productService.getProducts().subscribe({
      next: (res) => {
        this.dataSource.data = res;
        this.dataSource.paginator = this.paginator;
        this._progressBarService.complete();
      },
      error: (err) => {
        this._snackbarService.show(err.message);
        this._progressBarService.complete();
      }
    });
  }

  goToProductDetail(product: Product) {
    this._router.navigate([`/dashboard/product-detail/${product.id}`]);
  }
  deleteProduct(product: Product) {
    const dialogRef = this._matDialog.open(ConfirmationDialogComponent, {
      width: '400px',
      data: {
        message: 'Are you sure you want to delete this item?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == 'yes' || result == 'true') {
        // User clicked "Yes"
        this._progressBarService.start();
        this._productService.deleteProduct(product.id).subscribe({
          next: (res) => {
            this.getProducts();
            this._snackbarService.show('Product has been deleted');
            this._progressBarService.complete();
          },
          error: (err) => {
            this._snackbarService.show(err.message);
            this._progressBarService.complete();
          }
      })
      } else {
        // User clicked "No" or closed the dialog

      }
    });
  }
}
