import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NumberConversion } from './models/number-conversion';
import { NumberPipe } from '../app.numberpipe';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  protected baseUrl: string;
  protected httpClient: HttpClient;
  protected numberPipe: NumberPipe;

  public conversion: NumberConversion = new NumberConversion();
  public number: string | null = null;

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
    let inputNumber: number = +(this.number ?? "");

    // ensure correct min/max range
    if (inputNumber < +inputField.min) this.number = inputField.min;
    if (inputNumber > +inputField.max) this.number = inputField.max;
    inputNumber = +(this.number ?? "");

    // send request and handle result
    this.httpClient.get<NumberConversion>(this.baseUrl + `converter/${inputNumber}`)
      .subscribe({
        next: result => { this.conversion = result; },
        error: error => { this.conversion = new NumberConversion(); console.error(error); }
      });

    // format input field
    this.number = this.numberPipe.transform(this.number, "1.2-2", "de-DE");
    inputField.value = this.number ?? "";
  }
}
