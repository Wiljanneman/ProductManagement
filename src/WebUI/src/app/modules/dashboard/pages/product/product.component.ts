import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/Product';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from './product.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
})
export class ProductComponent implements OnInit {
  displayedColumns = ['id','name','description','incVatAmount','excVatAmount','qty','isSale', 'actions'];
  dataSource = new MatTableDataSource<Product>();
  constructor(private _productService: ProductService, private _snackbarService: SnackbarService) {

  }

  ngOnInit(): void {
    this.getProducts();
  }
  getProducts() {
    this._productService.getProducts().subscribe({
      next: (res) => {
        this.dataSource.data = res;
      },
      error: (err) => {
        this._snackbarService.show(err);
      }
    });
  }
  goToProductDetail(product: Product) {

  }
}
