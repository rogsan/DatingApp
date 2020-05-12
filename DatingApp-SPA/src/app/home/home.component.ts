import { Component, OnInit } from '@angular/core';
import { ValueService } from '../shared/services/value.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  isRegisterMode = true;
  values: any;

  constructor(private valueService: ValueService) { }

  ngOnInit() {
    this.getValues();
  }

  registerToggle() {
    this.isRegisterMode = !this.isRegisterMode;
  }

  getValues() {
    this.valueService.getValues().subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

  cancelRegisterMode(isRegisterMode: boolean) {
    this.isRegisterMode = isRegisterMode;
  }
}
