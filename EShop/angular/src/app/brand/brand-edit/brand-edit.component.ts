import { Component, OnInit } from '@angular/core';
import {first} from "rxjs/operators";
import {AlertService, BrandService, UserService} from "../../_services";
import {ActivatedRoute, Router} from "@angular/router";
import {Brand} from "../../_models/brand";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-brand-edit',
  templateUrl: './brand-edit.component.html',
  styleUrls: ['./brand-edit.component.css']
})
export class BrandEditComponent implements OnInit {

  brand = new Brand();
  id: number;
  editBrandForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private brandService: BrandService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router,
              private userService: UserService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getBrand(this.id);
    console.log(this.brand);
    this.editBrandForm = this.formBuilder.group({
      id: [this.brand.id, Validators.required],
      name: [this.brand.name, Validators.required],
      description: [this.brand.description, Validators.required],
      imageUrl: [this.brand.imageUrl, Validators.required],
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editBrandForm.controls; }

  private getBrand(id: number) {
    this.brandService.getById(id).pipe(first()).subscribe(brand => {
      this.brand.id = brand.id;
      this.brand.name = brand.name;
      this.brand.description = brand.description;
      this.brand.imageUrl = brand.imageUrl;
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editBrandForm.invalid) {
      return;
    }

    this.loading = true;
    this.brandService.update(this.editBrandForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Изменение бренда прошло успешно', true);
          this.router.navigate(['/brand-list']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
