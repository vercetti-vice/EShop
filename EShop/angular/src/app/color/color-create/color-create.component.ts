import { Component, OnInit } from '@angular/core';
import {first} from "rxjs/operators";
import {AlertService, BrandService, UserService} from "../../_services";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Brand} from "../../_models/brand";
import {Router} from "@angular/router";
import {Color} from "../../_models/color";
import {ColorService} from "../../_services/color.service";

@Component({
  selector: 'app-color-create',
  templateUrl: './color-create.component.html',
  styleUrls: ['./color-create.component.css']
})
export class ColorCreateComponent implements OnInit {

  color: Color;
  createColorForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private colorService: ColorService,
              private formBuilder: FormBuilder,
              private router: Router,
              private userService: UserService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.createColorForm = this.formBuilder.group({
      name: ['', Validators.required],
      hexCode: ['', Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.createColorForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.createColorForm.invalid) {
      return;
    }

    this.loading = true;
    this.colorService.create(this.createColorForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Создание цвета прошло успешно', true);
          this.router.navigate(['/color-list']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
