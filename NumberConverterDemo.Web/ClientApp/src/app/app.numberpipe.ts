import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Pipe({
  name: 'numberPipe'
})
export class NumberPipe implements PipeTransform {
  constructor(private decimalPipe: DecimalPipe) { }

  transform(value: number | string | null, digitsInfo?: string, locale?: string): string | null {
    let result = this.decimalPipe.transform(value, digitsInfo, locale);
    return (result ?? "").replace(/\./g, '');
  }
}
