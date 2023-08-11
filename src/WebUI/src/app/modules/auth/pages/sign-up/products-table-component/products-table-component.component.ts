import { Component, EventEmitter, Output, ViewChild } from '@angular/core';

@Component({
  selector: 'app-products-table-component',
  templateUrl: './products-table-component.component.html',
  styleUrls: ['./products-table-component.component.scss']
})
export class ProductsTableComponentComponent {
  @Output() selected = new EventEmitter<number>();


  productSelect(val: number) {
    this.selected.emit(val)
  }

}
