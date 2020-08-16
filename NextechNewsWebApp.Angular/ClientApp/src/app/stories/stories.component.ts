import { Component, Inject, ViewChild } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { MatTableDataSource } from '@angular/material';
import { MatPaginator, PageEvent } from '@angular/material';
import { Story } from './story';

@Component({
    selector: 'app-stories',
    templateUrl: './stories.component.html',
    styleUrls: ['./stories.component.css']
})
/** stories component*/
export class StoriesComponent {

  stories: MatTableDataSource<Story>;
  public displayedColumns: string[] = ['id', 'title', 'url'];
  public filter = "";

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private _baseUrl: string) {
  }

  getData(event: PageEvent) {
    var url = this._baseUrl + 'api/Story';
    var params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("filter", this.filter);

    this.http.get<any>(url, { params })
      .subscribe(result => {
        this.stories = new MatTableDataSource(result.data);
        this.paginator.length = result.count;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
      }, error => console.error(error));
  }

  ngOnInit() {
    var _pageEvent = new PageEvent();
    _pageEvent.pageIndex = 0;
    _pageEvent.pageSize = 10;

    this.getData(_pageEvent);
  }

  ngAfterViewInit() {
    if (this.stories) {
      this.stories.paginator = this.paginator;
    }
  }

  applyFilter(filterValue: string) {
    var _pageEvent = new PageEvent();
    _pageEvent.pageIndex = 0;
    _pageEvent.pageSize = 10;
    filterValue = filterValue.toLowerCase(); // Datasource defaults to lowercase matches
    this.filter = filterValue;
    this.getData(_pageEvent);
  }
}
