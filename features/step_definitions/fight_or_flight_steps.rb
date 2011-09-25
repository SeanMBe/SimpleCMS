Given /^the ninja has ([^"]*)$/ do |skill|
	Given %{I am on "home"}
	And %{I follow "Calculator"}
	And %{I select "#{skill}" from "my_ninja"}
end

When /^attacked by ([^"]*)$/ do |opponent|
  Given %{I select "#{opponent}" from "opponent"}
  When  %{I press "Calculate"}
end

Then /^the ninja should engage the opponent$/ do
  Then  %{I should see "Engage opponent"}
end

Then /^the ninja should run for his life$/ do
  Then  %{I should see "Run for his life"}
end


Given /^I have the following opponents with skills:$/ do |table|
 # table is a Cucumber::Ast::Table
  db = SQLite3::Database.new( "C:/temp/ninjacommander.db" )
  db.execute( "delete from Opponent" )  
  table.hashes.each do | row |
	db.execute( "insert into Opponent ('Description', 'Strength') values ('#{row['fighter']}', '#{row['skill']}')" )  
  end
  db.close
end

Given /^I have the following ninjas with skills:$/ do |table|
 # table is a Cucumber::Ast::Table
  db = SQLite3::Database.new( "C:/temp/ninjacommander.db" )
  db.execute( "delete from Ninja" )  
  table.hashes.each do | row |
	db.execute( "insert into Ninja ('Description', 'Strength') values ('#{row['fighter']}', '#{row['skill']}')" )  
  end
  db.close
end