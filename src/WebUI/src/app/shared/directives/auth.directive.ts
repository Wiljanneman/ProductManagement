/* eslint-disable @angular-eslint/use-lifecycle-interface */
/* eslint-disable @typescript-eslint/member-ordering */
import { Inject, Directive, ViewContainerRef, ElementRef, Input, OnChanges, SimpleChanges } from '@angular/core';
import { AuthService } from 'src/app/modules/auth/auth.service';



@Directive({ selector: '[mtRoles]' })
export class AuthDirective implements OnChanges {
    constructor( @Inject(ElementRef) _el: ElementRef,
     private _viewContainer: ViewContainerRef, private _authService: AuthService) {
        this.domNode = _el.nativeElement;
    }

    @Input() roles: string | undefined;
    private domNode: HTMLElement;

    ngOnInit() {
        this.checkRoles();
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes['roles'] && !changes['roles'].isFirstChange()) {
            this.checkRoles();
        }
    }

    private checkRoles() {
        if (this.roles === undefined || this.roles === null) {
            return;
        }

        if (this.roles.length !== 0 && this.roles !== 'All' && !this._authService.isInRole(this.roles)) {
            this.domNode.parentElement?.removeChild(this.domNode);
        }
    }

}
