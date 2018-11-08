import { Component, OnInit } from '@angular/core';
import {first} from "rxjs/operators";
import {AlertService, BrandService, UserService} from "../../_services";
import {ActivatedRoute, Router} from "@angular/router";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Brand} from "../../_models/brand";
import {Color} from "../../_models/color";
import {ColorService} from "../../_services/color.service";

@Component({
  selector: 'app-color-edit',
  templateUrl: './color-edit.component.html',
  styleUrls: ['./color-edit.component.css']
})
export class ColorEditComponent implements OnInit {

  color = new Color();
  id: number;
  editColorForm: FormGroup;
  loading = false;
  submitted = false;

  constructor(private colorService: ColorService,
              private activateRoute: ActivatedRoute,
              private formBuilder: FormBuilder,
              private router: Router,
              private userService: UserService,
              private alertService: AlertService) { }

  ngOnInit() {
    this.id = this.activateRoute.snapshot.params['id'];
    this.getColor(this.id);
    this.editColorForm = this.formBuilder.group({
      id: [this.color.id, Validators.required],
      name: [this.color.name, Validators.required],
      hexCode: [this.color.hexCode, Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.editColorForm.controls; }

  private getColor(id: number) {
    this.colorService.getById(id).pipe(first()).subscribe(color => {
      this.color.id = color.id;
      this.color.name = color.name;
      this.color.hexCode = color.hexCode;
    });
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.editColorForm.invalid) {
      return;
    }

    this.loading = true;
    this.colorService.update(this.editColorForm.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Изменение цвета прошло успешно', true);
          this.router.navigate(['/color-list']);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

}
