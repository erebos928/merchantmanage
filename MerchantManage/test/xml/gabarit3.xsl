<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE html>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:fo="http://www.w3.org/1999/XSL/Format" xmlns:xs="http://www.w3.org/2001/XMLSchema" xmlns:fn="http://www.w3.org/2005/xpath-functions">
  <xsl:variable name="cats">
    <entry key="1">Appliances</entry>
    <entry key="1.1">Home Appliaces</entry>
    <entry key="1.1.1">Dishwashers</entry>
    <entry key="1.1.2">Freezers</entry>
    <entry key="1.2">Small Appliance</entry>
    <entry key="1.2.1">Coffee Makers</entry>
    <entry key="1.2.2">Microwaves</entry>
    <entry key="2">Clothings</entry>
    <entry key="2.1">Men</entry>
    <entry key="2.2">Women</entry>
    <entry key="2.2.1">Coats</entry>
    <entry key="2.3">Boys</entry>
    <entry key="2.3.1">Boys Bottom</entry>
    <entry key="2.3.1.1">Boys Pants</entry>
    <entry key="2.3.1.2">Boys Denim</entry>
    <entry key="2.4">Girls</entry>
    <entry key="2.4.1">Girls Bottom</entry>
    <entry key="2.4.1.1">Girls Pants</entry>
    <entry key="2.4.1.2">Girls Denim</entry>
    <entry key="2.5">Shoes</entry>
    <entry key="2.5.1">Girls Shoes</entry>
    <entry key="2.5.2">Boys Shoes</entry>
    <entry key="2.5.3">Men Shoes</entry>
    <entry key="2.5.4">Women Shoes</entry>

  </xsl:variable>
  <xsl:variable name="feats">
    <entry key="f.1">Title</entry>
    <entry key="f.2">Size</entry>
    <entry key="f.3">Color</entry>
    <entry key="f.4">price</entry>
    <entry key="f.5">Currency</entry>
    <entry key="f.6">Voltage</entry>
    <entry key="f.7">Brand</entry>
    <entry key="f.8">Weight</entry>
    <entry key="f.9">Description</entry>
  </xsl:variable>

  <xsl:template match="/Catalogue/Zone">
    <html lang="en">
      <head>
        <meta charset="utf-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1"/>
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script>
$(document).ready(function(){
       $("#articlecount").load("/Head/ArticleCount");})
              </script>
      </head>
      <body>
        <div class="container">
          <div class="row">
            <div class="col-md-4 text-center">
              <a>
                <img src="{Logo}" width="180" height="100" alt=""></img>
              </a>
              <br></br>
              <xsl:value-of select="@preferredName"/>
            </div>
            <div class="col-md-4"></div>
            <div class="col-md-4 text-right"> <button type="button" class="btn btn-default btn-sm">
          <span class="glyphicon glyphicon-shopping-cart"></span>Shopping Cart <span class="badge" id="articlecount"></span>
        </button></div>
          </div>
        </div>
        <hr></hr>
        <div class="container">
          <div class="row">
            <xsl:for-each select="Division">
              <div class="col-md-4 text-center">
                <a>
                  <img src="{Logo}" width="180" height="100" alt=""></img>
                </a>
                <br></br>
                <xsl:value-of select="@preferredName"/>
              </div>
            </xsl:for-each>
          </div>
        </div>
        <hr></hr>
        <div class="container">
          <div class="row">
             <xsl:apply-templates select="./Division/Category"></xsl:apply-templates>
            
          </div>
        </div>
      </body>
    </html>
  </xsl:template>
  <xsl:template match="Category">
       <div class="container">
          <div class="row">
            <div class="col-md-4 text-center">
	   <xsl:choose>
      <xsl:when test="@preferredName">
          <a	href="?currentnode={@id}">
            <xsl:value-of select="@preferredName"/>
          </a>
      </xsl:when>
      <xsl:otherwise>
        <xsl:variable name="identifier" select="attribute::id"/>
           <a href="?currentnode={@id}">
            <xsl:value-of select="$cats/entry[@key=$identifier]"></xsl:value-of>
          </a>
       </xsl:otherwise>
    </xsl:choose>
    </div>
    </div>
    </div>
    <hr></hr>
    <xsl:apply-templates></xsl:apply-templates>
  </xsl:template>
  <xsl:template match="Item">
<xsl:variable name="identifiant" select="attribute::id"></xsl:variable>    
    <div class="col-md-4">
    <a class="btn btn-info">Add to carte</a>
    <div class="panel-group">
    <div class="panel panel-default">
<xsl:for-each select="Feature">
<xsl:if test="position() eq 1">    
<!-- for first feature-->  
    <div class="panel-heading">
    <h4 class="panel-title">
<!-- inserting value of feature-->        
            <xsl:variable name="fid" select="attribute::id"/>
                <xsl:choose>
                  <xsl:when test="@preferredName">
                    <a data-toggle="collapse" href="#col-{$identifiant}"><xsl:value-of select="@preferredName"/></a>
                  </xsl:when>
                  <xsl:otherwise>
                    <a data-toggle="collapse" href="#col-{$identifiant}"><xsl:value-of select="$feats/entry[@key=$fid]"/></a>
                  </xsl:otherwise>
                </xsl:choose>
                <a data-toggle="collapse" href="#col-{$identifiant}"><xsl:value-of  select="./text()"></xsl:value-of></a>
      </h4>    
     </div>

 <!-- end of inserting value of feature-->
 <!-- end of inserting first feature-->
 </xsl:if>
 <!-- else if features other than first one-->
</xsl:for-each>
     <div id="col-{$identifiant}" class="panel-collapse collapse">
      <ul class="list-group">
        <xsl:for-each select="Feature">     
             <xsl:variable name="fid" select="attribute::id"/>
					<li class="list-group-item">
                <xsl:choose>
                  <xsl:when test="@preferredName">
				 <xsl:value-of select="@preferredName"/>
				 </xsl:when>
				 <xsl:otherwise>
					 <xsl:value-of select="$feats/entry[@key=$fid]"/>
				 </xsl:otherwise>
				 </xsl:choose>
				 <xsl:value-of  select="./text()"/></li>
  </xsl:for-each>
  </ul>
  </div>
  </div>
  </div>     
<div class="col-md-2">     
         <xsl:for-each select="Image">
				<img src="{./text()}" alt="" width="170" height="200"></img>
        </xsl:for-each>
</div>				     
</div>
  </xsl:template>


</xsl:stylesheet>