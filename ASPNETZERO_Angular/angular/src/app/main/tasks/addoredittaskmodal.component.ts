import { Component, OnInit, ViewChild, Output, Injector, EventEmitter } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { TaskServiceProxy, TaskListDto, TaskEditDto } from '@shared/service-proxies/service-proxies';
import { TaskState } from '@shared/AppEnums';
export interface ITaskOnEdit {
    id?: number;
    title?: string;
    description?: string;
    state?: string;
}

@Component({
    selector: 'addOrEditTaskModal',
    templateUrl: './addoredittaskmodal.component.html',
    styleUrls: ['./addoredittaskmodal.component.css']
})
export class AddOrEditTaskModalComponent extends AppComponentBase {
    @ViewChild('createOrEditModal') modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    active = false;
    saving = false;
    task: TaskEditDto = new TaskEditDto();
    //selectedState: number;
    stateSelectOptions = [
        { text: this.l('TaskState_Open'), value: TaskState.Open },
        { text: this.l('TaskState_Completed'), value: TaskState.Completed },
    ]
    constructor(injector: Injector, private taskService: TaskServiceProxy) {
        super(injector);
    }

    show(taskId?: number): void {
        const self = this;
        self.active = true;

        self.taskService.getTaskForEdit(taskId).subscribe(result => {
            self.task = result.task;
            self.modal.show();
        });
    }


    //show(): void {
    //    this.active = true;
    //    this.modal.show();
    //}
    //save(): void {
    //    this.saving = true;
    //    this.taskService.create(this.task)
    //        .finally(() => { this.saving = false; })
    //        .subscribe(result => {
    //            this.modalSave.emit(result);
    //            this.close();
    //        });
    //}


    save(): void {
        if (!this.task.id) {
            this.createTask();
        } else {
            this.updateTask();
        }
    }

    createTask() {
       //    this.saving = true;
    //    this.taskService.create(this.task)
    //        .finally(() => { this.saving = false; })
    //        .subscribe(result => {
    //            this.modalSave.emit(result);
    //            this.close();
    //        });
        const createInput = new TaskEditDto();
        createInput.title = this.task.title;
        createInput.description = this.task.description;
        createInput.state = this.task.state; //as any
        this.saving = true;
        this.taskService
            .create(createInput)
            .finally(() => this.saving = false)
            .subscribe(result => {
                this.modalSave.emit(result);
                this.close();
            });
    }

    updateTask() {
        const updateInput = new TaskEditDto();
        updateInput.id = this.task.id;
        updateInput.title = this.task.title;
        updateInput.description = this.task.description;
        updateInput.state = this.task.state; //as any
        this.saving = true;
        this.taskService
            .updateTask(updateInput)
            .finally(() => this.saving = false)
            .subscribe(result => {
                this.modalSave.emit(result);
                this.close();
            });
    }



    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
