import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NumberConversion } from './models/number-conversion';
import { NumberPipe } from '../app.numberpipe';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  protected baseUrl: string;
  protected httpClient: HttpClient;
  protected numberPipe: NumberPipe;

  public conversion: NumberConversion = new NumberConversion();

  constructor(
    httpClient: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    numberPipe: NumberPipe) {
    this.baseUrl = baseUrl;
    this.httpClient = httpClient;
    this.numberPipe = numberPipe;
  }

  handleChange(event: Event) {
    let inputField = event.target as HTMLInputElement;
    let inputNumber: number = +(inputField.value ?? "");

    // ensure correct min/max range
    if (inputNumber < +inputField.min) inputField.value = inputField.min;
    if (inputNumber > +inputField.max) inputField.value = inputField.max;
    inputNumber = +(inputField.value ?? "");

    // send request and handle result
    this.conversion.number = inputNumber;
    this.httpClient.get<NumberConversion>(this.baseUrl + `converter/${inputNumber}`)
      .subscribe({
        next: result => { this.conversion = result; },
        error: error => { this.conversion = new NumberConversion(); console.error(error); }
      });

    // format input field
    inputField.value = this.numberPipe.transform(inputField.value, "1.2-2", "de-DE") ?? "";
  }
}
