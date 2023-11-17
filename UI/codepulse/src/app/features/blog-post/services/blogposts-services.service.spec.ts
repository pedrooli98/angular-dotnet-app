import { TestBed } from '@angular/core/testing';

import { BlogpostsServicesService } from './blogposts-services.service';

describe('BlogpostsServicesService', () => {
  let service: BlogpostsServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BlogpostsServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
