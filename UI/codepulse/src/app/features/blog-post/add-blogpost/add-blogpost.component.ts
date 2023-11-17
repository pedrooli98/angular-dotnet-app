import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.models';
import { Observable, Subscription } from 'rxjs';
import { BlogPostsService } from '../services/blogposts-services.service';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent implements OnDestroy, OnInit{
  model :AddBlogPost
  private addBlogPostSubscription?: Subscription
  categories$?: Observable<Category[]> 

  constructor(
    private service: BlogPostsService,
    private router: Router, 
    private categoryService: CategoryService) {
      this.model = {
        title : '',
        shortDescription: '',
        urlHandle: '',
        content: '',
        featuredImageUrl: '',
        author: '',
        isVisible: true,
        publishedDate: new Date(),
        categories: []
      }
  }
  
  onFormSubmit(): void {
    this.addBlogPostSubscription = this.service.addBlogPost(this.model)
    .subscribe({
      next: (response) => {
        this.router.navigateByUrl('/admin/blogposts')
      }
    })
  }
  
  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories()
  }
  
  ngOnDestroy(): void {
    this.addBlogPostSubscription?.unsubscribe();
  }
}
