import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { Story } from './story';

@Component({
    selector: 'app-stories',
    templateUrl: './stories.component.html',
    styleUrls: ['./stories.component.css']
})
/** stories component*/
export class StoriesComponent implements OnDestroy, OnInit {

  public dtOptions: DataTables.Settings = {};
  public dtTrigger: Subject<Story[]> = new Subject();
  public stories: Story[];

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string) {
  }

  ngOnInit() {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 10
    };
    this.http.get<Story[]>(this._baseUrl + 'api/Story')
      .subscribe(result => {
        this.stories = result;
        this.dtTrigger.next();
      }, error => console.error(error));
  }

  ngOnDestroy(): void {
    this.dtTrigger.unsubscribe();
  }
}
