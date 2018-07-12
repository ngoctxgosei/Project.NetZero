import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { EmployeeServiceProxy, EmployeeListDto } from '@shared/service-proxies/service-proxies';
import { ActivatedRoute } from '@angular/router';
import { LazyLoadEvent  } from 'primeng/primeng';
import { Paginator } from 'primeng/components/paginator/paginator';
import { DataTable } from 'primeng/components/datatable/datatable';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AddoreditemployeemodalComponent } from '@app/main/employees/addoreditemployeemodal.component';
@Component({
    selector: 'app-employees',
    templateUrl: './employees.component.html',
    styleUrls: ['./employees.component.css'],
    animations: [appModuleAnimation()]
})
export class EmployeesComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditEmployeeModal') createOrEditEmployeeModal: AddoreditemployeemodalComponent;
    @ViewChild('dataTable') dataTable: DataTable;
    @ViewChild('paginator') paginator: Paginator;
    //Filters
    filterText = '';
    constructor(
        injector: Injector,
        private _employeeService: EmployeeServiceProxy,
        private _activatedRoute: ActivatedRoute
    ) {
        super(injector)
        this.filterText = this._activatedRoute.snapshot.queryParams['filterText'] || '';
    }
    getEmployees(event?: LazyLoadEvent) {
        if (this.primengDatatableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengDatatableHelper.showLoadingIndicator();

        this._employeeService.getEmployees(
            this.filterText,
            this.primengDatatableHelper.getSorting(this.dataTable),
            this.primengDatatableHelper.getMaxResultCount(this.paginator, event),
            this.primengDatatableHelper.getSkipCount(this.paginator, event)
        ).subscribe(result => {
            //this.primengDatatableHelper.totalRecordsCount = result.totalCount;
            this.primengDatatableHelper.records = result.items;
            this.primengDatatableHelper.hideLoadingIndicator();
        });
    }
    ngOnInit() {
    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    //exportToExcel(): void {
    //    this._userServiceProxy.getUsersToExcel()
    //        .subscribe(result => {
    //            this._fileDownloadService.downloadTempFile(result);
    //        });
    //}

    createEmployee(): void {
        this.createOrEditEmployeeModal.show();
    }

    deleteEmployee(emp: EmployeeListDto): void {
        this.message.confirm(
            this.l('EmployeeDeleteWarningMessage', emp.name),
            (isConfirmed) => {
                if (isConfirmed) {
                    this._employeeService.deleteEmployee(emp.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }
}
