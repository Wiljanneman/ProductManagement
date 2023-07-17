import { AfterViewInit, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../product.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { Product } from '../../models/Product';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit, AfterViewInit {
  productForm: FormGroup;
  pageType: 'new' | 'edit' = 'new';
  currentProductId: number = 0;
  product!: Product;

  constructor(
    private formBuilder: FormBuilder,
    private _route: ActivatedRoute,
    private _productService: ProductService,
    private _snackbarService: SnackbarService,
    private _router: Router,
    private _matDialog: MatDialog) {
    this.productForm = this.formBuilder.group({
      id: [0],
      name: ['', Validators.required],
      description: '',
      regularPrice: [0, [Validators.required, Validators.min(0)]],
      salePrice: [0, Validators.min(0)],
      quantity: [0, [Validators.required, Validators.min(0)]],
      isOnSale: false,
      includingVATAmount: { value: '', disabled: true },
      vATAmount: [0.15]
    });
  }
  ngAfterViewInit(): void {
    // show required fields
    this.productForm.markAllAsTouched();
    this.getProductVat();
  }
  ngOnInit() {
    this.productForm.get('regularPrice')?.valueChanges.subscribe(val => {
      this.calculateVatIncluded();
    });
    this.productForm.get('salePrice')?.valueChanges.subscribe(val => {
      this.calculateVatIncluded();
    });
    this.productForm.get('isOnSale')?.valueChanges.subscribe(val => {
      this.calculateVatIncluded();
    });
    this._route.params.subscribe(params => {
      const id = params['id'];
      if (id === 'new') {
        this.pageType = 'new';
      } else {
        this.currentProductId = +id;
        this.pageType = 'edit';
        this.getProductById(+id);
      }
    });
  }
  calculateVatIncluded() {
    let formValues: Product = this.productForm.getRawValue();
    if (formValues.isOnSale) {
      this.productForm.patchValue({
        includingVATAmount: ((1 + (this.productForm.get('vATAmount')?.value / 100)) * formValues.salePrice).toFixed(2)
      });
    } else {
      this.productForm.patchValue({
        includingVATAmount: ((1 + (this.productForm.get('vATAmount')?.value / 100)) * formValues.regularPrice).toFixed(2)
      });
    }
  }
  getProductVat() {
    this._productService.getVat().subscribe({
      next: (res) => {
        this.productForm.patchValue({
          vATAmount: res
        });
      },
      error: (err) => {
        this._snackbarService.show(err.message);
      }
    });
  }
  getProductById(id: number) {
    this._productService.getProductById(id).subscribe({
      next: (res) => {
        this.product = res;
        this.productForm.patchValue(res);
      },
      error: (err) => {
        this._snackbarService.show(err.message);
      }
    });
  }
  createProduct() {
    if (!this.productForm.valid) {
      this._snackbarService.show('Ensure all required fields are completed');
      return;
    }
    this._productService.addProduct(this.productForm.getRawValue()).subscribe({
      next: (res) => {
        this._router.navigate(['/dashboard/products'])
        this._snackbarService.show('Product has been updated');
      },
      error: (err) => {
        this._snackbarService.show(err.message);
      }
    })
  }
  updateProduct() {
    if (!this.productForm.valid) {
      this._snackbarService.show('Ensure all required fields are completed');
      return;
    }
    this._productService.updateProduct(this.productForm.getRawValue()).subscribe({
      next: (res) => {
        this._router.navigate(['/dashboard/products'])
        this._snackbarService.show('Product has been added');
      },
      error: (err) => {
        this._snackbarService.show(err.message);
      }
    })
  }
  openConfirmationDialog(): void {
    const dialogRef = this._matDialog.open(ConfirmationDialogComponent, {
      width: '400px',
      data: {
        message: 'Are you sure you want to delete this item?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == 'yes') {
        // User clicked "Yes"
        this._productService.deleteProduct(this.product.id).subscribe({
          next: (res) => {
            this._router.navigate(['/dashboard/products'])
            this._snackbarService.show('Product has been deleted');
          },
          error: (err) => {
            this._snackbarService.show(err.message);
          }
        })
      }
    });
  }

}
