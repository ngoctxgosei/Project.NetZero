import { Component, OnInit, ViewChild, Output, EventEmitter, Injector, ElementRef, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { EmployeeEditDto, EmployeeServiceProxy, DemoUiComponentsServiceProxy, CreateOrUpdateEmployeeInput } from '@shared/service-proxies/service-proxies';
import * as moment from 'moment';
@Component({
    selector: 'createOrEditEmployeeModal',
  templateUrl: './addoreditemployeemodal.component.html',
  styleUrls: ['./addoreditemployeemodal.component.css']
})
export class AddoreditemployeemodalComponent extends AppComponentBase{
    
    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('Birthday') birthday: ElementRef;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    isSubscriptionFieldsVisible = false;
    emp: EmployeeEditDto = new EmployeeEditDto();
    constructor(
        injector: Injector,
        private _empService: EmployeeServiceProxy,
        private demoUiComponentsService: DemoUiComponentsServiceProxy
    ) {
        super(injector);
    }
    //ngOnInit() {
    //    $(this.birthday.nativeElement).datetimepicker({
    //        locale: abp.localization.currentLanguage.name,
    //        format: 'L'
    //    });

    //}
    //ngAfterViewInit(): void {
    //    $(this.birthday.nativeElement).datetimepicker({
    //        locale: abp.localization.currentLanguage.name,
    //        format: 'L'
    //    });
    //}
    show(empId?: number): void {
        const self = this;
        self.active = true;

        this._empService.getEmployeeForEdit(empId).subscribe(empResult => {
            this.emp = empResult.employee;
                this.modal.show();
        });
    }
    onShown(): void {
        //$(this.birthday.nativeElement).datetimepicker({
        //    locale: abp.localization.currentLanguage.name,
        //    format: 'L'
        //});

        $(this.birthday.nativeElement).datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L',
            defaultDate: this.emp.birthday,
        });
    }
    close(): void {
        this.active = false;
        this.modal.hide();
    }
    birthdayIsValid(): boolean {

        if (!this.birthday) {
            return false;
        }

        let birthday = $(this.birthday.nativeElement).val();
        return birthday !== undefined && birthday !== '';
    }
    save(): void {
        let input = new CreateOrUpdateEmployeeInput();
        this.emp.birthday = moment($(this.birthday.nativeElement).data('DateTimePicker').date().format('YYYY-MM-DDTHH:mm:ss') + 'Z');
        input.employee = this.emp;
        this.saving = true;
        this._empService.createOrUpdateEmployee(input)
            .finally(() => { this.saving = false; })
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }
}
