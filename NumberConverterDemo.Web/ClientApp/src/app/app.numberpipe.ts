import { Pipe, PipeTransform } from '@angular/core';
import { DecimalPipe } from '@angular/common';

@Pipe({
  name: 'numberPipe'
})
export class NumberPipe implements PipeTransform {
  constructor(private decimalPipe: DecimalPipe) { }

  transform(value: number | string | null, digitsInfo?: string, locale?: string): string | null {
    if (navigator.userAgent.toLowerCase().includes('firefox')) {
      // firefox needs a localized value
      let result = this.decimalPipe.transform(value, digitsInfo, locale);
      return (result ?? "").replace(/\./g, '');
    } else {
      // chromium needs a invariant value and applies localized format itself
      let result = this.decimalPipe.transform(value, digitsInfo);
      return (result ?? "").replace(/,/g, '');
    }
  }
}
