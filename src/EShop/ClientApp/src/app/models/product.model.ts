import {Brand} from './brand.model';
import {Category} from './category.model';

export class Product {
  public id: number;
  public name: string;
  public categoryId: number;
  public brandId: number;
  public price: number;
  public description: string;
  public rating: number;
  public imageUrl: string;

  public brand: Brand;
  public category: Category;
}
