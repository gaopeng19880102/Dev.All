/*
 * Licensed to Jasig under one or more contributor license
 * agreements. See the NOTICE file distributed with this work
 * for additional information regarding copyright ownership.
 * Jasig licenses this file to you under the Apache License,
 * Version 2.0 (the "License"); you may not use this file
 * except in compliance with the License.  You may obtain a
 * copy of the License at the following location:
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
//package org.jasig.cas.authentication;

//import java.util.Date;
//import java.util.HashMap;

//import org.jasig.cas.authentication.principal.Principal;

/**
 * Mutable implementation of Authentication interface.
 * <p>
 * Instanciators of the MutableAuthentication class must take care that the map
 * they provide is serializable (i.e. HashMap).
 * 
 * @author Scott Battaglia
 * @version $Revision$ $Date$
 * @since 3.0.3
 */
public  class MutableAuthentication : AbstractAuthentication {

    /** Unique Id for serialization. */
    private static  long serialVersionUID = -4415875344376642246L;

    /** The date/time this authentication object became valid. */
    private  Date authenticatedDate;

    public MutableAuthentication( Principal principal) {
        this(principal, new Date());
    }
    
    public MutableAuthentication( Principal principal,  Date date) {
        super(principal, new HashMap<string, Object>());
        this.authenticatedDate = date;
    }

    public Date getAuthenticatedDate() {
        return this.authenticatedDate;
    }
}