import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { debug } from 'console';
@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.css']
})
export class OrderComponent {
  public restaurants: Restaurant[];
  public query: string = "";
  public overAllPrice: number = 0;
  public error: string;
  http: HttpClient;
  baseUrl: string;
  form: FormGroup;
  menuItems: MenuItem[]

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private fb: FormBuilder) {
    this.form = this.fb.group({
      checkArray: this.fb.array([])
    })
    this.http = http;
    this.baseUrl = baseUrl;
  }

  onCheckboxChange(e) {
    const checkArray: FormArray = this.form.get('checkArray') as FormArray;

    if (e.target.checked) {
      checkArray.push(new FormControl(e.target.value));
      this.IncreaseOverallPrice(e.target.value)
    } else {
      let i: number = 0;
      checkArray.controls.forEach((item: FormControl) => {
        if (item.value == e.target.value) {
          this.DecreaseOverallPrice(e.target.value)
          checkArray.removeAt(i);
          return;
        }
        i++;
      });
    }
  }
  IncreaseOverallPrice(itemId: number) {
    this.overAllPrice += this.menuItems.filter(mi => mi.id == itemId)[0].price;
  }
  DecreaseOverallPrice(itemId: number){
    this.overAllPrice -= this.menuItems.filter(mi => mi.id == itemId)[0].price;
  }
  public doSearch() {
    this.http.get<Restaurant[]>(this.baseUrl + 'food?search=' + this.query).subscribe(result => {
      console.log(result)
      this.error = null;
      this.restaurants = result;
      this.menuItems = this.restaurants.reduce((c, r) => [...c, ...r.categories], []).reduce((mi, c) => [...mi, ...c.menuItems], []);
    }, error => {
        console.error(error);
        this.restaurants = [];
        this.error = error.error;
    });
  }

  submitForm() {
    console.log(this.form.value)
    var body = { menuItemIds: this.form.value.checkArray };
    this.http.post<Restaurant[]>(this.baseUrl + 'orders', body).subscribe(result => {
      console.log(result);
    }, error => {
        console.error(error);
    });
  }
}

export interface MenuItem {
  id: number;
  name: string;
  price: number;
}

export interface Category {
  name: string;
  menuItems: MenuItem[];
}

export interface Restaurant {
  id: number;
  name: string;
  city: string;
  suburb: string;
  logoPath: string;
  rank: number;
  categories: Category[];
}
