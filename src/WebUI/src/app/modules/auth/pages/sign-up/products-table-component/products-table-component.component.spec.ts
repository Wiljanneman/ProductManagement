import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsTableComponentComponent } from './products-table-component.component';

describe('ProductsTableComponentComponent', () => {
  let component: ProductsTableComponentComponent;
  let fixture: ComponentFixture<ProductsTableComponentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductsTableComponentComponent]
    });
    fixture = TestBed.createComponent(ProductsTableComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
