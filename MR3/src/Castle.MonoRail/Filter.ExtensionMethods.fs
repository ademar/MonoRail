﻿//  Copyright 2004-2012 Castle Project - http://www.castleproject.org/
//  Hamilton Verissimo de Oliveira and individual contributors as indicated. 
//  See the committers.txt/contributors.txt in the distribution for a 
//  full listing of individual contributors.
// 
//  This is free software; you can redistribute it and/or modify it
//  under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 3 of
//  the License, or (at your option) any later version.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this software; if not, write to the Free
//  Software Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA
//  02110-1301 USA, or see the FSF site: http://www.fsf.org.

namespace Castle.MonoRail

    [<System.Runtime.CompilerServices.ExtensionAttribute>]
    module public ExtensionMethods = 
         
        open System
        open System.Collections.Generic
        open System.Reflection
        open System.Web
        open System.ComponentModel.Composition
        open Castle.MonoRail.Routing
        open Castle.MonoRail.Framework
        open Castle.MonoRail.Hosting.Mvc
        open Castle.MonoRail.Hosting.Mvc.Extensibility
        open Castle.MonoRail.Hosting.Mvc.Typed
        open System.Runtime.CompilerServices

        let private get_list (route:Route) =
            if not <| route.ExtraData.ContainsKey(Constants.MR_Filters_Key) then
                route.ExtraData.[Constants.MR_Filters_Key] <- List<FilterDescriptor>()
            route.ExtraData.[Constants.MR_Filters_Key] :?> List<FilterDescriptor>

        let private getNextOrder<'a> (descriptors:List<FilterDescriptor>) = 
            if Seq.isEmpty descriptors 
            then 1
            else (descriptors |> Seq.maxBy (fun d -> if d.Applies<'a>() then d.Order else 0 )).Order + 1
        

        [<ExtensionAttribute>]
        [<CompiledName("WithActionFilterOrdered")>]
        let WithActionFilterWithOrder<'filter>(route:Route, order:int) = 
            let descriptors = get_list route
            descriptors.Add <| FilterDescriptor.IncludeType(typeof<'filter>, order, (fun _ -> ()))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithActionFilter")>]
        let WithActionFilter<'filter>(route:Route) = 
            let descriptors = get_list route
            WithActionFilterWithOrder(route, (getNextOrder<IActionFilter>(descriptors))) 
            
            

        
        
        (*
        [<ExtensionAttribute>]
        [<CompiledName("WithActionFilterInstance")>]
        let WithActionFilterInstance(route:Route, filter:IActionFilter) = 
            let descriptors = get_list route
            let order = getNextOrder<IActionFilter>(descriptors)
            descriptors.Add(FilterDescriptor.IncludeInstance(filter, order))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithActionFilterInstance")>]
        let WithActionFilterInstance(route:Route, filter:IActionFilter, order:int) = 
            let descriptors = get_list route
            descriptors.Add(FilterDescriptor.IncludeInstance(filter, order))
            route





        [<ExtensionAttribute>]
        [<CompiledName("WithAuthorizationFilter")>]
        let WithAuthorizationFilter<'filter>(route:Route) = 
            // let descriptors = get_list route
            // descriptors.Add(FilterDescriptor(typeof<'filter>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithAuthorizationFilter")>]
        let WithAuthorizationFilterOrder<'filter>(route:Route, order:int) = 
            // let descriptors = get_list route
            // descriptors.Add(FilterDescriptor(typeof<'filter>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithAuthorizationFilterInstance")>]
        let WithAuthorizationFilterInstance(route:Route, filter:IAuthorizationFilter) = 
            // let descriptors = get_list route
            // descriptors.Add(FilterDescriptor(typeof<'filter>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithAuthorizationFilterInstance")>]
        let WithAuthorizationFilterInstance(route:Route, filter:IAuthorizationFilter, order:int) = 
            // let descriptors = get_list route
            // descriptors.Add(FilterDescriptor(typeof<'filter>))
            route
        




        [<ExtensionAttribute>]
        [<CompiledName("WithExceptionFilter")>]
        let WithExceptionFilter<'filter>(route:Route) = 
            // let descriptors = get_list route
            // descriptors.Add(ExceptionFilterDescriptor(typeof<'filter>, typeof<'excp>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithExceptionFilter")>]
        let WithExceptionFilter2<'filter>(route:Route, order:int) = 
            // let descriptors = get_list route
            // descriptors.Add(ExceptionFilterDescriptor(typeof<'filter>, typeof<'excp>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithExceptionFilterInstance")>]
        let WithExceptionFilterInstance(route:Route, filter:IExceptionFilter) = 
            // let descriptors = get_list route
            // descriptors.Add(ExceptionFilterDescriptor(typeof<'filter>, typeof<'excp>))
            route

        [<ExtensionAttribute>]
        [<CompiledName("WithExceptionFilterInstance")>]
        let WithExceptionFilterInstance2(route:Route, filter:IExceptionFilter, order:int) = 
            // let descriptors = get_list route
            // descriptors.Add(ExceptionFilterDescriptor(typeof<'filter>, typeof<'excp>))
            route



*)